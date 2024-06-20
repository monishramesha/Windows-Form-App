// src/routes.ts
import { Request, Response } from 'express';
import fs from 'fs-extra';
import { WorkflowItem, WorkflowContainer } from './types'; // Ensure the correct path to types.ts

const DB_FILE = './db.json';

export const submitForm = (req: Request, res: Response) => {
    const submission: WorkflowItem = req.body as WorkflowItem;

    console.log('Received submission:', submission);

    // Read existing data from db.json
    let workflowContainer: WorkflowContainer = { Workflows: [] };
    try {
        if (fs.existsSync(DB_FILE)) {
            workflowContainer = fs.readJsonSync(DB_FILE);
        }
    } catch (error) {
        console.error('Error reading db.json:', error);
        res.status(500).json({ error: 'Failed to read workflows' });
        return;
    }

    // Add new workflow
    try {
        if (!workflowContainer.Workflows) {
            workflowContainer.Workflows = [];
        }
        workflowContainer.Workflows.push(submission);

        // Write updated data back to db.json
        fs.writeJsonSync(DB_FILE, workflowContainer, { spaces: 2 });
        console.log('Workflow added successfully:', submission);
        res.status(201).json({ message: 'Workflow added successfully' });
    } catch (error) {
        console.error('Error writing to db.json:', error);
        res.status(500).json({ error: 'Failed to save workflow' });
    }
};


export const readForm = (req: Request, res: Response) => {
    let workflowContainer: WorkflowContainer = { Workflows: [] };

    // Read workflows from db.json
    try {
        if (fs.existsSync(DB_FILE)) {
            workflowContainer = fs.readJsonSync(DB_FILE);
        }
    } catch (error) {
        console.error('Error reading db.json:', error);
        res.status(500).json({ error: 'Failed to read workflows' });
        return;
    }

    console.log('Retrieved workflows:', workflowContainer.Workflows);

    res.json(workflowContainer.Workflows);
};

export const deleteForm = (req: Request, res: Response) => {
    const em = req.params.email;

    //read existing data from db.json
    let workflowContainer: WorkflowContainer = { Workflows: [] };
    try {
        if (fs.existsSync(DB_FILE)) {
            workflowContainer = fs.readJsonSync(DB_FILE);
        }
    } catch (error) {
        console.error('Error reading db.json:', error)
        res.status(500).json({ error: 'Failed to read workflows' });
        return;
    }

    //find index of submission to delete
    const index = workflowContainer.Workflows.findIndex(item => item.Email === em);
    if (index === -1) {
        res.status(404).json({ error: 'Submission not found' });
        return;
    }

    workflowContainer.Workflows.splice(index, 1);

    try {
        fs.writeJsonSync(DB_FILE, workflowContainer, { spaces: 2 });
        console.log('Workflow deleted successfully');
        res.status(204).end();
    } catch (error) {
        console.error('Error writing to db.json: ', error);
        res.status(500).json({ error: 'Failed to delete workflow' });
    }
};

export const editForm = (req: Request, res: Response) => {
    const em = req.params.email; // Extract submission ID from request params
    const updatedSubmission: WorkflowItem = req.body as WorkflowItem; // Updated submission data

    // Read existing data from db.json
    let workflowContainer: WorkflowContainer = { Workflows: [] };
    try {
        if (fs.existsSync(DB_FILE)) {
            workflowContainer = fs.readJsonSync(DB_FILE);
        }
    } catch (error) {
        console.error('Error reading db.json:', error);
        res.status(500).json({ error: 'Failed to read workflows' });
        return;
    }

    // Find index of the submission to update
    const index = workflowContainer.Workflows.findIndex(item => item.Email === em);
    if (index === -1) {
        res.status(404).json({ error: 'Submission not found' });
        return;
    }

    // Update the submission
    workflowContainer.Workflows[index] = updatedSubmission;

    // Write updated data back to db.json
    try {
        fs.writeJsonSync(DB_FILE, workflowContainer, { spaces: 2 });
        console.log('Workflow updated successfully:', updatedSubmission);
        res.status(200).json(updatedSubmission); // Respond with updated submission
    } catch (error) {
        console.error('Error writing to db.json:', error);
        res.status(500).json({ error: 'Failed to update workflow' });
    }
};

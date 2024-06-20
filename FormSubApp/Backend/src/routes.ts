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

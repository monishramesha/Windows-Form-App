// src/routes.ts
import { Request, Response } from 'express';
import fs from 'fs-extra';
import { Submission } from './types'; // Ensure the correct path to types.ts
import path from 'path';

const DB_FILE = path.join(__dirname, 'db.json');

export const submitForm = (req: Request, res: Response) => {
    const submission: Submission = req.body as Submission; // Ensure req.body is correctly typed as Submission

    // Read existing data from db.json
    let submissions: Submission[] = [];
    try {
        if (fs.existsSync(DB_FILE)) {
            submissions = fs.readJsonSync(DB_FILE);
        } else {
            fs.writeJsonSync(DB_FILE, submissions, { spaces: 2 });
        }
    } catch (error) {
        console.error('Error reading db.json:', error);
        res.status(500).json({ error: 'Failed to read submissions' });
        return;
    }

    // Add new submission
    submissions.push(submission);

    // Write updated data back to db.json
    try {
        fs.writeJsonSync(DB_FILE, submissions, { spaces: 2 });
        res.status(201).json({ message: 'Submission added successfully' });
    } catch (error) {
        console.error('Error writing to db.json:', error);
        res.status(500).json({ error: 'Failed to save submission' });
    }
};

export const readForm = (req: Request, res: Response) => {
    const { index } = req.query;
    if (typeof index !== 'string') {
        res.status(400).json({ error: 'Invalid index parameter' });
        return;
    }

    const submissionIndex = Number(index);
    if (isNaN(submissionIndex)) {
        res.status(400).json({ error: 'Invalid index parameter' });
        return;
    }

    // Read submissions from db.json
    let submissions: Submission[] = [];
    try {
        if (fs.existsSync(DB_FILE)) {
            submissions = fs.readJsonSync(DB_FILE);
        }
    } catch (error) {
        console.error('Error reading db.json:', error);
        res.status(500).json({ error: 'Failed to read submissions' });
        return;
    }

    // Validate index range
    if (submissionIndex < 0 || submissionIndex >= submissions.length) {
        res.status(404).json({ error: 'Submission not found' });
        return;
    }

    const submission = submissions[submissionIndex];
    res.json(submission);
};

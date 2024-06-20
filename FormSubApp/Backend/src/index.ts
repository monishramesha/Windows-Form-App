// src/index.ts
import express from 'express';
import bodyParser from 'body-parser';
import fs from 'fs-extra';
import { WorkflowItem, WorkflowContainer } from './types';
import { submitForm, readForm } from './routes';

const app = express();
const PORT = 3000;

app.use(express.json());

// Routes
app.get('/ping', (req, res) => {
    res.json({ success: true });
});

app.post('/submit', submitForm);

app.get('/read', readForm);

// Start server
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});

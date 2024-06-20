// src/index.ts
import express from 'express';
import { WorkflowItem, WorkflowContainer } from './types';
import { submitForm, readForm, deleteForm } from './routes';

const app = express();
const PORT = 3000;

app.use(express.json());

// Routes
app.get('/ping', (req, res) => {
    res.json({ success: true });
});

app.post('/submit', submitForm);

app.get('/read', readForm);

app.delete('/delete/:id', deleteForm);

app.put('/edit/:id', editForm); // PUT endpoint with ID parameter for editing


// Start server
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});

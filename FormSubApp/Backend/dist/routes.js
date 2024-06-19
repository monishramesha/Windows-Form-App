"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.readForm = exports.submitForm = void 0;
const fs_extra_1 = __importDefault(require("fs-extra"));
const DB_FILE = './src/db.json';
const submitForm = (req, res) => {
    const submission = req.body;
    let submissions = [];
    try {
        submissions = fs_extra_1.default.readJsonSync(DB_FILE);
    }
    catch (error) {
        console.error('Error reading db.json:', error);
        res.status(500).json({ error: 'Failed to read submissions' });
        return;
    }
    submissions.push(submission);
    try {
        fs_extra_1.default.writeJsonSync(DB_FILE, submissions, { spaces: 2 });
        res.status(201).json({ message: 'Submission added successfully' });
    }
    catch (error) {
        console.error('Error writing to db.json:', error);
        res.status(500).json({ error: 'Failed to save submission' });
    }
};
exports.submitForm = submitForm;
const readForm = (req, res) => {
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
    let submissions = [];
    try {
        submissions = fs_extra_1.default.readJsonSync(DB_FILE);
    }
    catch (error) {
        console.error('Error reading db.json:', error);
        res.status(500).json({ error: 'Failed to read submissions' });
        return;
    }
    if (submissionIndex < 0 || submissionIndex >= submissions.length) {
        res.status(404).json({ error: 'Submission not found' });
        return;
    }
    const submission = submissions[submissionIndex];
    res.json(submission);
};
exports.readForm = readForm;
//# sourceMappingURL=routes.js.map
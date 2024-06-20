// src/types.ts
export interface WorkflowItem {
    Name: string;
    Email: string;
    Phone: string;
    GitHubLink: string;
    StopwatchTime: string;
}

export interface WorkflowContainer {
    Workflows: WorkflowItem[];
}

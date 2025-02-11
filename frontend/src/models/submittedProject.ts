import { Company } from './company';

export interface SubmittedProject {
  id: number;
  name: string;
  description: string;
  dueDate: string;
  technologies: string[];
  position: string;
  workArrangement: string;
  recruiterId: number;
  recruiter: string;
  company: Company;
  projectUrl: string;
  submittedAt: string;
}

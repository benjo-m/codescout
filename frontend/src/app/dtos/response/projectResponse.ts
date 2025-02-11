import { Project } from 'src/models/project';

export interface ProjectResponse {
  page: number;
  totalPages: number;
  count: number;
  projects: Project[];
}

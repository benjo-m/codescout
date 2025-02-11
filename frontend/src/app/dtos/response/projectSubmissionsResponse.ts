import { SubmittedProjectMinimal } from 'src/models/submittedProjectMinimal';

export interface ProjectSubmissionsResponse {
  projectId: number;
  recruiterId: number;
  submittedProjects: SubmittedProjectMinimal[];
}

export interface CreateProjectRequest {
  recruiterId: number,
  name: string,
  description: string,
  dueDate: string,
  technologies: string[],
  position: string,
  workArrangement: string
}

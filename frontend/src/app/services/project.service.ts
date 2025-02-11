import { Injectable } from '@angular/core';
import { Project } from 'src/models/project';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CompanyResponse } from '../dtos/response/companyResponse';
import { TechnologyResponse } from '../dtos/response/technologyResponse';
import { PositionResponse } from '../dtos/response/positionsResponse';
import { ProjectResponse } from '../dtos/response/projectResponse';
import { SubmitProjectRequest } from '../dtos/request/submitProjectRequest';
import { SubmittedProject } from 'src/models/submittedProject';
import { CreateProjectRequest } from '../dtos/request/createProjectRequest';
import { WorkArrangementsResponse } from '../dtos/response/workArrangementsResponse';
import { AuthService } from './auth.service';
import { ProjectSubmissionsResponse } from '../dtos/response/projectSubmissionsResponse';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  URL = 'https://localhost:7025/api';

  selectedCompanies: string[] = [];
  selectedTechnologies: string[] = [];
  selectedPositions: string[] = [];

  constructor(private auth: AuthService, private http: HttpClient) {}

  appendParams(page: number) {
    let queryParams = new HttpParams();

    queryParams = queryParams.append('page', page);

    for (let i = 0; i < this.selectedCompanies.length; i++) {
      queryParams = queryParams.append('companies', this.selectedCompanies[i]);
    }

    for (let i = 0; i < this.selectedTechnologies.length; i++) {
      queryParams = queryParams.append(
        'technologies',
        this.selectedTechnologies[i]
      );
    }

    for (let i = 0; i < this.selectedPositions.length; i++) {
      queryParams = queryParams.append('positions', this.selectedPositions[i]);
    }

    return queryParams;
  }

  getProjects(page: number, myProjectsOnly: boolean = false) {
    return this.http.get<ProjectResponse>(`${this.URL}/Project`, {
      params: this.appendParams(page),
    });
  }

  getProjectById(projectId: number) {
    return this.http.get<Project>(`${this.URL}/Project/${projectId}`);
  }

  getProjectsByUser(userId: number) {
    let token: string | null = localStorage.getItem('token');

    return this.http.get<SubmittedProject[]>(
      `${this.URL}/Project/user/${userId}`
    );
  }

  getSubmittedProjectById(projectId: number, userId: number) {
    return this.http.get<SubmittedProject>(
      `${this.URL}/Project/user/submitted-projects?projectId=${projectId}&userId=${userId}`
    );
  }

  getCompanies() {
    return this.http.get<CompanyResponse>(`${this.URL}/Resource/companies`);
  }

  getTechnologies() {
    return this.http.get<TechnologyResponse>(
      `${this.URL}/Resource/technologies`
    );
  }

  getPostions() {
    return this.http.get<PositionResponse>(`${this.URL}/Resource/positions`);
  }

  getWorkArrangements() {
    return this.http.get<WorkArrangementsResponse>(
      `${this.URL}/Resource/workArrangements`
    );
  }

  getSubmissionsByProject(projectId: number) {
    return this.http.get<ProjectSubmissionsResponse>(
      `${
        this.URL
      }/Project/GetSubmissionsByProject?projectId=${projectId}&recruiterId=${this.auth.getUserId()}`
    );
  }

  addCompany(companyName: string) {
    if (
      this.selectedCompanies.length == 0 ||
      this.selectedCompanies.indexOf(companyName) == -1
    ) {
      this.selectedCompanies.push(companyName);
    }
  }

  addTechnology(technologyName: string) {
    if (
      this.selectedTechnologies.length == 0 ||
      this.selectedTechnologies.indexOf(technologyName) == -1
    ) {
      this.selectedTechnologies.push(technologyName);
    }
  }

  addPosition(positionName: string) {
    if (
      this.selectedPositions.length == 0 ||
      this.selectedPositions.indexOf(positionName) == -1
    ) {
      this.selectedPositions.push(positionName);
    }
  }

  removeCompanyTag(tag: any) {
    let companyName = tag.textContent.trim();
    let index = this.selectedCompanies.indexOf(companyName);
    this.selectedCompanies.splice(index, 1);
  }

  removeTechnologyTag(tag: any) {
    let technologyName = tag.textContent.trim();
    let index = this.selectedTechnologies.indexOf(technologyName);
    this.selectedTechnologies.splice(index, 1);
  }

  removePositionTag(tag: any) {
    let positionName = tag.textContent.trim();
    let index = this.selectedPositions.indexOf(positionName);
    this.selectedPositions.splice(index, 1);
  }

  submitProject(submitProjectRequest: SubmitProjectRequest) {
    return this.http.post(
      `${this.URL}/Project/submit-project`,
      submitProjectRequest
    );
  }

  createProject(createProjectRequest: CreateProjectRequest) {
    return this.http.post<Project>(`${this.URL}/Project`, createProjectRequest);
  }

  doesProjectBelongToUser(projectId: number, userId: number) {
    return this.http.get<boolean>(
      `${this.URL}/Project/DoesProjectBelongToUser?projectId=${projectId}&userId=${userId}`
    );
  }

  getProjectsByRecruiter(recruiterId: number) {
    return this.http.get<Project[]>(
      `${this.URL}/Project/recruiter/${recruiterId}`
    );
  }

  deleteProject(projectId: number) {
    return this.http.delete(`${this.URL}/Project/${projectId}`);
  }
}

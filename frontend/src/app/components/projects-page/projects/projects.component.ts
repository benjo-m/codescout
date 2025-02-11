import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { CompanyResponse } from '../../../dtos/response/companyResponse';
import { TechnologyResponse } from '../../../dtos/response/technologyResponse';
import { PositionResponse } from '../../../dtos/response/positionsResponse';
import { ProjectResponse } from '../../../dtos/response/projectResponse';
import { Project } from 'src/models/project';
import { Company } from 'src/models/company';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css'],
})
export class ProjectsComponent implements OnInit {
  isRecruiter: boolean = false;

  projects: Project[] = [];

  companyList: Company[] = [];
  technologyList: string[] = [];
  positionList: string[] = [];

  myProjectsOnly: boolean = this.isRecruiter;
  selectedCompanies: string[] = [];
  selectedTechnologies: string[] = [];
  selectedPositions: string[] = [];

  page = 1;
  totalPages = 1;

  constructor(
    private authService: AuthService,
    private projectService: ProjectService
  ) {}

  ngOnInit(): void {
    this.selectedCompanies = this.projectService.selectedCompanies;
    this.selectedTechnologies = this.projectService.selectedTechnologies;
    this.selectedPositions = this.projectService.selectedPositions;

    this.authService.isRecruiter().subscribe((value) => {
      this.isRecruiter = value;
      this.getProjects(this.page);
    });
    this.populateDropdowns();
  }

  getProjects(page: number) {
    if (this.isRecruiter == true) {
      this.projectService
        .getProjectsByRecruiter(this.authService.getUserId())
        .subscribe((data) => {
          this.projects = data;
        });
    } else {
      this.projectService
        .getProjects(page, this.myProjectsOnly)
        .subscribe((data: ProjectResponse) => {
          this.projects = data.projects;
          this.totalPages = data.totalPages;
        });
    }
  }

  deleteProject(projectId: number) {
    this.projectService.deleteProject(projectId).subscribe((x) => {
      this.projectService
        .getProjectsByRecruiter(this.authService.getUserId())
        .subscribe((data) => {
          this.projects = data;
        });
    });
  }

  populateDropdowns() {
    this.projectService.getCompanies().subscribe((data: CompanyResponse) => {
      this.companyList = data.companies;
    });

    this.projectService
      .getTechnologies()
      .subscribe((data: TechnologyResponse) => {
        this.technologyList = data.technologies;
      });

    this.projectService.getPostions().subscribe((data: PositionResponse) => {
      this.positionList = data.positions;
    });
  }

  nextPage() {
    if (this.page != this.totalPages) {
      this.page++;
      this.getProjects(this.page);
    }
  }

  prevPage() {
    if (this.page != 1) {
      this.page--;
      this.getProjects(this.page);
    }
  }

  showMyProjectsOnlyChange() {
    this.getProjects(this.page);
  }

  addCompany(companyName: string) {
    this.projectService.addCompany(companyName);
    this.getProjects(this.page);
  }

  addTechnology(technologyName: string) {
    this.projectService.addTechnology(technologyName);
    this.getProjects(this.page);
  }

  addPosition(positionName: string) {
    this.projectService.addPosition(positionName);
    this.getProjects(this.page);
  }

  removeCompanyTag(tag: any) {
    this.projectService.removeCompanyTag(tag);
    this.getProjects(this.page);
  }

  removeTechnologyTag(tag: any) {
    this.projectService.removeTechnologyTag(tag);
    this.getProjects(this.page);
  }

  removePositionTag(tag: any) {
    this.projectService.removePositionTag(tag);
    this.getProjects(this.page);
  }
}

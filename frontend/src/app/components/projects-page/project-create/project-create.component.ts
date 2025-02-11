import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { CreateProjectRequest } from '../../../dtos/request/createProjectRequest';
import { Company } from '../../../../models/company';
import { AuthService } from '../../../services/auth.service';
import { User } from '../../../../models/user';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-project-create',
  templateUrl: './project-create.component.html',
  styleUrls: ['./project-create.component.css'],
})
export class ProjectCreateComponent implements OnInit {
  isRecruiter: boolean = false;

  dueDate: string = new Date().toISOString().substring(0, 10);

  companies: Company[] = [];
  technologies: string[] = [];
  positions: string[] = [];
  workArrangements: string[] = [];

  project: CreateProjectRequest = {
    recruiterId: this.authService.getUserId(),
    description: '',
    dueDate: '',
    name: '',
    position: '',
    technologies: [],
    workArrangement: '',
  };

  constructor(
    private authService: AuthService,
    private projectService: ProjectService,
    private router: Router,
    private datePipe: DatePipe
  ) {}

  ngOnInit() {
    this.authService.isRecruiter().subscribe((value) => {
      if (value) this.isRecruiter = true;
      else this.router.navigateByUrl(`projects`);
    });

    this.projectService.getTechnologies().subscribe((value) => {
      this.technologies = value.technologies;
    });

    this.projectService.getPostions().subscribe((value) => {
      this.positions = value.positions;

      if (value.positions.length) this.project.position = value.positions[0];
    });

    this.projectService.getWorkArrangements().subscribe((value) => {
      this.workArrangements = value.workArrangements;

      if (this.workArrangements.length)
        this.project.workArrangement = value.workArrangements[0];
    });
  }

  selectPosition(position: string) {
    this.project.position = position;
  }

  selectWorkArrangement(workArrangement: string) {
    this.project.workArrangement = workArrangement;
  }

  selectTechnology(technology: string) {
    let existingElementIndex = this.project.technologies.indexOf(technology);

    if (existingElementIndex >= 0) {
      this.project.technologies.splice(existingElementIndex, 1);
    } else {
      this.project.technologies.push(technology);
    }
  }

  isInputValid() {
    if (!this.project.name.length) {
      alert('You need to input project name');
      return false;
    } else if (!this.project.technologies.length) {
      alert('Select at least one technology');
      return false;
    } else if (new Date(this.dueDate) <= new Date()) {
      alert('Due date must be higher than current date');
      return false;
    } else if (!this.project.description.length) {
      alert('Fill in the description of the project');
      return false;
    }

    return true;
  }

  createProject() {
    if (this.isInputValid()) {
      let date: string | null = this.datePipe.transform(
        this.dueDate,
        'yyyy-MM-dd'
      );

      if (date) {
        this.project.dueDate = date;

        this.projectService.createProject(this.project).subscribe((value) => {
          this.router.navigateByUrl(`projects/${value.id}`);
        });
      }
    }
  }
}

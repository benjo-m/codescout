import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { ActivatedRoute } from '@angular/router';
import { Project } from 'src/models/project';
import { SubmittedProject } from 'src/models/submittedProject';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-submitted-project-details',
  templateUrl: './submitted-project-details.component.html',
  styleUrls: ['./submitted-project-details.component.css'],
})
export class SubmittedProjectDetailsComponent implements OnInit {
  project: SubmittedProject | null = null;

  constructor(
    private projectService: ProjectService,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    let projectId = this.activatedRoute.snapshot.params['id'];

    this.projectService
      .getSubmittedProjectById(projectId, this.authService.getUserId())
      .subscribe((data: SubmittedProject) => {
        this.project = data;
      });
  }
}

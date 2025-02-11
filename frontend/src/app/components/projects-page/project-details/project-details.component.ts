import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../../services/project.service';
import { Project } from 'src/models/project';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { SubmitProjectRequest } from '../../../dtos/request/submitProjectRequest';
import { ProjectSubmissionsResponse } from '../../../dtos/response/projectSubmissionsResponse';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css'],
})
export class ProjectDetailsComponent implements OnInit {
  project: Project | null = null;
  isRecruiter: boolean = false;
  isMyProject: boolean = false;
  showSubmissions: boolean = false;
  applyToggle = false;
  repoLink: string = '';
  projectSubmissions: ProjectSubmissionsResponse | null = null;
  submitProjectRequest: SubmitProjectRequest = {
    projectId: -1,
    userId: -1,
    projectUrl: '',
  };

  constructor(
    private projectService: ProjectService,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    let projectId = this.activatedRoute.snapshot.params['id'];

    this.projectService.getProjectById(projectId).subscribe((data: Project) => {
      this.project = data;
      this.submitProjectRequest!.projectId = data.id;
      this.submitProjectRequest!.userId = this.authService.getUserId();
    });

    this.authService.isRecruiter().subscribe((value) => {
      this.isRecruiter = value;

      if (this.isRecruiter) {
        this.projectService
          .doesProjectBelongToUser(projectId, this.authService.getUserId())
          .subscribe((value1) => {
            this.isMyProject = value1;
            console.log(this.isMyProject);

            if (this.isMyProject) {
              this.projectService
                .getSubmissionsByProject(projectId)
                .subscribe((value2) => {
                  this.projectSubmissions = value2;
                  console.log(this.projectSubmissions);
                });
            }
          });
      }
    });
  }

  toggleApply() {
    this.applyToggle = !this.applyToggle;
  }

  isValidGithubLink(link: string) {
    // Regular expression to match GitHub repository URL
    const githubRepoPattern =
      /^(https?:\/\/)?(www\.)?github\.com\/[a-zA-Z0-9-]+\/[a-zA-Z0-9-_]+(\/)?$/;

    if (githubRepoPattern.test(link)) {
      this.submitProjectRequest!.projectUrl = link;
      return true;
    }

    return false;
  }

  submitProject() {
    this.projectService.submitProject(this.submitProjectRequest!).subscribe({
      next: () => {
        alert('Successfully submitted');
        this.router.navigate(['projects']);
      },
      error: () => {
        alert('Project already submitted');
      },
    });
  }

  btnShowSubmissions(element: HTMLElement) {
    this.showSubmissions = true;
    this.scrollTo(element);
  }

  scrollTo(element: HTMLElement) {
    element.scrollIntoView({ behavior: 'smooth', block: 'start' });
  }
}

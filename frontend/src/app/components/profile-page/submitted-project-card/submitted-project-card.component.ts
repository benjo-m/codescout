import { Component, Input } from '@angular/core';
import { Project } from 'src/models/project';

@Component({
  selector: 'app-submitted-project-card',
  templateUrl: './submitted-project-card.component.html',
  styleUrls: ['./submitted-project-card.component.css'],
})
export class SubmittedProjectCardComponent {
  @Input() project: any;
}

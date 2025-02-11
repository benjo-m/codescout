import { Component, Input, OnInit } from '@angular/core';
import { EditUserRequest } from '../../../dtos/request/editUserRequest';
import { UserStats } from 'src/models/userStats';
import { User } from 'src/models/user';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css'],
})
export class EditUserComponent implements OnInit {
  editUserRequest = {} as EditUserRequest;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.getUser().subscribe((user: User) => {
      this.setUserInfo(user);
    });
  }

  setUserInfo(user: User) {
    this.editUserRequest = {
      userId: this.authService.getUserId(),
      username: user.username,
      email: user.email,
      biography: user.biography,
      password: '',
      twoFactorEnabled: user.twoFactorEnabled,
      gitHub: user.socials.gitHub,
      linkedIn: user.socials.linkedIn,
      medium: user.socials.medium,
      stackOverflow: user.socials.stackOverflow,
      x: user.socials.x,
    };
  }

  checkLinks() {
    const githubProfilePattern =
      /^(https?:\/\/)?(www\.)?github\.com\/[a-zA-Z0-9-]+(\/)?$/;
    const stackOverflowProfilePattern =
      /^(https?:\/\/)?(www\.)?stackoverflow\.com\/users\/\d+\/[a-zA-Z0-9-]+(\/)?$/;
    const mediumProfilePattern =
      /^(https?:\/\/)?(www\.)?medium\.com\/@[a-zA-Z0-9-]+(\/)?$/;
    const linkedInProfilePattern =
      /^(https?:\/\/)?(www\.)?linkedin\.com\/in\/[a-zA-Z0-9-]+(\/)?$/;
    const xProfilePattern =
      /^(https?:\/\/)?(www\.)?(x|twitter)\.com\/[a-zA-Z0-9_]+(\/)?$/;

    if (
      !githubProfilePattern.test(this.editUserRequest.gitHub) &&
      this.editUserRequest.gitHub
    ) {
      alert('GitHub profile link invalid');
      return false;
    } else if (
      !linkedInProfilePattern.test(this.editUserRequest.linkedIn) &&
      this.editUserRequest.linkedIn
    ) {
      alert('LinkedIn profile link invalid');
      return false;
    } else if (
      !stackOverflowProfilePattern.test(this.editUserRequest.stackOverflow) &&
      this.editUserRequest.stackOverflow
    ) {
      alert('StackOverflow profile link invalid');
      return false;
    } else if (
      !xProfilePattern.test(this.editUserRequest.x) &&
      this.editUserRequest.x
    ) {
      alert('X profile link invalid');
      return false;
    } else if (
      !mediumProfilePattern.test(this.editUserRequest.medium) &&
      this.editUserRequest.medium
    ) {
      alert('Medium profile link invalid');
      return false;
    } else return true;
  }

  saveChanges() {
    if (this.checkLinks()) {
      this.authService
        .editUser(this.editUserRequest)
        .subscribe((x) => this.router.navigate(['/profile']));
    }
  }

  discardChanges() {
    this.router.navigate(['/profile']);
  }
}

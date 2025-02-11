import { Component } from '@angular/core';
import { SignInRequest } from '../../../dtos/request/signInRequest';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
})
export class SigninComponent {
  signInRequest: SignInRequest = { username: '', password: '' };

  constructor(private authService: AuthService, private router: Router) {}

  signIn(signInRequest: SignInRequest) {
    this.authService.signIn(signInRequest).subscribe((data) => {
      if (data) {
        if (data.token == null) {
          this.router.navigate([`2fa/${data.userId}`]);
        } else {
          window.localStorage.setItem('token', data.token);
          window.localStorage.setItem(
            'token-expiration-date',
            JSON.stringify(data.tokenExpirationDate)
          );
          this.router.navigate(['projects']);
        }
      } else {
        alert('Wrong username or password.');
      }
    });
  }
}

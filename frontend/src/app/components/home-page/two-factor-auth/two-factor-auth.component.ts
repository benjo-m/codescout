import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TwoFactorRequest } from 'src/app/dtos/request/twoFactorRequest';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-two-factor-auth',
  templateUrl: './two-factor-auth.component.html',
  styleUrls: ['./two-factor-auth.component.css'],
})
export class TwoFactorAuthComponent implements OnInit {
  twoFactorRequest = {} as TwoFactorRequest;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.twoFactorRequest.userId = parseInt(
      this.route.snapshot.paramMap.get('userId')!
    );
  }

  submitOtp() {
    this.authService
      .twoFactorAuth(this.twoFactorRequest)
      .subscribe((data: any) => {
        if (!data) {
          alert('Wrong OTP code');
        } else {
          window.localStorage.setItem('token', data.token);
          window.localStorage.setItem(
            'token-expiration-date',
            JSON.stringify(data.tokenExpirationDate)
          );
          this.router.navigate(['projects']);
        }
      });
  }
}

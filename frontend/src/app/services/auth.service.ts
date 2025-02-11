import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SignInResponse } from '../dtos/response/signInResponse';
import { SignInRequest } from '../dtos/request/signInRequest';
import { User } from 'src/models/user';
import { RegisterUserRequest } from '../dtos/request/registerUserRequest';
import { Company } from 'src/models/company';
import { UserStats } from 'src/models/userStats';
import { EditUserRequest } from '../dtos/request/editUserRequest';
import { TwoFactorRequest } from '../dtos/request/twoFactorRequest';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  URL = 'https://localhost:7025/api';

  constructor(private http: HttpClient, private router: Router) {}

  signIn(signInRequest: SignInRequest) {
    return this.http.post<SignInResponse>(
      `${this.URL}/auth/signin`,
      signInRequest
    );
  }

  twoFactorAuth(twoFactorRequest: TwoFactorRequest) {
    return this.http.post<SignInResponse>(
      `${this.URL}/auth/2fa`,
      twoFactorRequest
    );
  }

  signOut() {
    const token = localStorage.getItem('token');

    localStorage.clear();

    const signOutRequest = { token: token };

    return this.http.post(`${this.URL}/auth/signout`, signOutRequest);
  }

  register(registerRequest: RegisterUserRequest) {
    return this.http.post<User>(`${this.URL}/user`, registerRequest);
  }

  registerCompany(company: Company) {
    return this.http.post<Company>(`${this.URL}/company`, company);
  }

  getToken(): string {
    let expiration = localStorage.getItem('token-expiration-date');

    if (!expiration) return '';

    let expirationFormatted = expiration?.slice(0, -1).substring(1);
    const expirationDate = new Date(expirationFormatted!);

    if (expirationDate <= new Date()) {
      localStorage.clear();
      this.router.navigate(['signin']);
    }

    return localStorage.getItem('token')!;
  }

  isExpiredToken(): boolean {
    let expiration = localStorage.getItem('token-expiration-date');
    let expirationFormatted = expiration?.slice(0, -1).substring(1);

    if (!expiration) return true;

    const expirationDate = new Date(expirationFormatted!);

    return expirationDate <= new Date();
  }

  getUserId(): number {
    const token = localStorage.getItem('token');
    const parts = token?.split(':')!;
    const userId = parseInt(parts[1]);
    return userId;
  }

  isRecruiter(userId: number = 0) {
    if (!userId) userId = this.getUserId();

    return this.http.get<boolean>(
      `${this.URL}/User/IsRecruiter?userId=${userId}`
    );
  }

  isUserRecruiter(user: User) {
    return user.company.name != null;
  }

  getUser(userToRetrieveId: number = 0) {
    const userId = userToRetrieveId ? userToRetrieveId : this.getUserId();
    return this.http.get<User>(`${this.URL}/user/${userId}`);
  }

  isSignedIn(): boolean {
    return localStorage.getItem('token') != null;
  }

  getUserStats(userId: number) {
    return this.http.get<UserStats>(`${this.URL}/user/stats?userId=${userId}`);
  }

  editUser(request: EditUserRequest) {
    return this.http.put<User>(`${this.URL}/user/edit`, request);
  }
}

import { Component, OnInit } from '@angular/core';
import countries from '../../../data/countries.json';
import { Company } from 'src/models/company';
import { HttpClient } from '@angular/common/http';
import { CompanyResponse } from '../../../dtos/response/companyResponse';
import { RegisterUserRequest } from '../../../dtos/request/registerUserRequest';
import { AuthService } from '../../../services/auth.service';
import { switchMap, tap } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  company: Company = {
    name: '',
    address: {
      country: '',
      city: '',
      street: '',
      postalCode: '',
    },
  };

  user: RegisterUserRequest = {
    username: '',
    email: '',
    password: '',
    role: '',
    company: '',
  };

  URL = 'https://localhost:7025/api';
  page: number = 1;
  countriesList = countries;
  companyList: Company[] = [];
  companyRegistered: boolean = true;

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.http
      .get<CompanyResponse>(`${this.URL}/Resource/companies`)
      .subscribe((data: CompanyResponse) => {
        this.companyList = data.companies;
      });
  }

  register(user: RegisterUserRequest, company: Company) {
    if (user.role == 'recruiter' && !this.companyRegistered) {
      this.authService
        .registerCompany(company)
        .pipe(
          tap((data: Company) => {
            this.user.company = data.name;
          }),
          switchMap(() => this.authService.register(user))
        )
        .subscribe();
    } else {
      this.authService.register(user).subscribe({
        next: () => alert('Account successfully created'),
        error: () => alert('Error'),
      });
    }

    this.router.navigate(['signin']);
  }

  nextButtonEnabled(): boolean {
    if (this.user.role == '') return false;

    if (this.user.role == 'candidate') return true;

    if (
      this.user.role == 'recruiter' &&
      this.companyRegistered &&
      this.user.company == ''
    )
      return false;

    if (!this.companyRegistered) {
      if (this.company.name == '') return false;
      if (this.company.address.country == '') return false;
      if (this.company.address.city == '') return false;
      if (this.company.address.street == '') return false;
      if (this.company.address.postalCode == '') return false;
    }
    return true;
  }

  registerButtonEnabled(): boolean {
    if (this.user.username == '') return false;
    if (this.user.email == '') return false;
    if (this.user.password == '') return false;
    return true;
  }

  previousPage() {
    if (this.page != 1) {
      this.page--;
    }
  }

  nextPage() {
    if (this.page != 2) {
      this.page++;
    }
  }
}

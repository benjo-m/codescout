import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeComponent } from './components/home-page/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RegisterComponent } from './components/home-page/register/register.component';
import { SigninComponent } from './components/home-page/signin/signin.component';
import { ProjectsComponent } from './components/projects-page/projects/projects.component';
import { CommonModule, DatePipe } from '@angular/common';
import { ProjectCardComponent } from './components/projects-page/project-card/project-card.component';
import { ProfileComponent } from './components/profile-page/profile/profile.component';
import { FormsModule } from '@angular/forms';
import { SocialComponent } from './components/social-page/social/social.component';
import { MessageCardComponent } from './components/social-page/message-card/message-card.component';
import { ProjectDetailsComponent } from './components/projects-page/project-details/project-details.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { SubmittedProjectCardComponent } from './components/profile-page/submitted-project-card/submitted-project-card.component';
import { SubmittedProjectDetailsComponent } from './components/profile-page/submitted-project-details/submitted-project-details.component';
import { EditUserComponent } from './components/profile-page/edit-user/edit-user.component';
import { DirectMessageComponent } from './components/social-page/direct-message/direct-message.component';
import { AuthInterceptor } from './interceptors/auth-interceptor';
import { ProjectCreateComponent } from './components/projects-page/project-create/project-create.component';
import { TwoFactorAuthComponent } from './components/home-page/two-factor-auth/two-factor-auth.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    RegisterComponent,
    SigninComponent,
    ProjectsComponent,
    ProjectCardComponent,
    ProfileComponent,
    SocialComponent,
    MessageCardComponent,
    ProjectDetailsComponent,
    SubmittedProjectCardComponent,
    SubmittedProjectDetailsComponent,
    EditUserComponent,
    DirectMessageComponent,
    ProjectCreateComponent,
    TwoFactorAuthComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    NgbModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    DatePipe,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

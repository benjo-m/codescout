import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home-page/home/home.component';
import { RegisterComponent } from './components/home-page/register/register.component';
import { SigninComponent } from './components/home-page/signin/signin.component';
import { ProjectsComponent } from './components/projects-page/projects/projects.component';
import { ProfileComponent } from './components/profile-page/profile/profile.component';
import { signedInGuard } from './guards/signed-in-guard.guard';
import { SocialComponent } from './components/social-page/social/social.component';
import { ProjectDetailsComponent } from './components/projects-page/project-details/project-details.component';
import { notSignedInGuard } from './guards/not-signed-in.guard';
import { SubmittedProjectDetailsComponent } from './components/profile-page/submitted-project-details/submitted-project-details.component';
import { EditUserComponent } from './components/profile-page/edit-user/edit-user.component';
import { DirectMessageComponent } from './components/social-page/direct-message/direct-message.component';
import { ProjectCreateComponent } from './components/projects-page/project-create/project-create.component';
import { TwoFactorAuthComponent } from './components/home-page/two-factor-auth/two-factor-auth.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [notSignedInGuard],
  },
  {
    path: 'register',
    component: RegisterComponent,
    title: 'Register',
    canActivate: [notSignedInGuard],
  },
  {
    path: 'signin',
    component: SigninComponent,
    title: 'Sign In',
    canActivate: [notSignedInGuard],
  },
  {
    path: '2fa/:userId',
    component: TwoFactorAuthComponent,
    title: 'Two factor autentication',
    canActivate: [notSignedInGuard],
  },
  {
    path: 'projects',
    component: ProjectsComponent,
    title: 'Projects',
    canActivate: [signedInGuard],
  },
  {
    path: 'projects/create',
    component: ProjectCreateComponent,
    title: 'Create Project',
    canActivate: [signedInGuard],
  },
  {
    path: 'projects/:id',
    component: ProjectDetailsComponent,
    title: 'Project Details',
    canActivate: [signedInGuard],
  },
  {
    path: 'profile',
    component: ProfileComponent,
    title: 'Profile',
    canActivate: [signedInGuard],
  },
  {
    path: 'profile/edit',
    component: EditUserComponent,
    title: 'Edit Profile',
    canActivate: [signedInGuard],
  },
  {
    path: 'profile/:id',
    component: ProfileComponent,
    title: 'Profile',
    canActivate: [signedInGuard],
  },
  {
    path: 'profile/project/:id',
    component: SubmittedProjectDetailsComponent,
    canActivate: [signedInGuard],
  },
  {
    path: 'social',
    component: SocialComponent,
    title: 'Social',
    canActivate: [signedInGuard],
  },
  {
    path: 'social/dm/:id',
    component: DirectMessageComponent,
    canActivate: [signedInGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

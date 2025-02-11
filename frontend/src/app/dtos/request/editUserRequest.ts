import { SocialMediaLinks } from 'src/models/socialMediaLinks';

export interface EditUserRequest {
  userId: number;
  username: string;
  email: string;
  password: string;
  biography: string;
  twoFactorEnabled: boolean;
  gitHub: string;
  linkedIn: string;
  stackOverflow: string;
  x: string;
  medium: string;
}

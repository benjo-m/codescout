import { Award } from './award';
import { Company } from './company';
import { SocialMediaLinks } from './socialMediaLinks';

export interface User {
  username: string;
  email: string;
  role: string;
  company: Company;
  awards: Award[];
  biography: string;
  twoFactorEnabled: boolean;
  socials: SocialMediaLinks;
}

import { Company } from 'src/models/company';

export interface RegisterUserRequest {
  username: string;
  email: string;
  password: string;
  role: string;
  company: string;
}

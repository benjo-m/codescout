import { Company } from 'src/models/company';

export interface CompanyResponse {
  count: number;
  companies: Company[];
}

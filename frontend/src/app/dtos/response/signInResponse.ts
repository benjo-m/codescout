export interface SignInResponse {
  userId: number;
  token: string;
  tokenExpirationDate: Date;
}

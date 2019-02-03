export interface UserModel {
  id: number;
  login: string;
  name: string;
  lastName: string;
  email: string;
  joinDate: Date;
  birthDate: Date;
  token: string;
}

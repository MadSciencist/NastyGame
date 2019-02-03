export interface UserModel {
  id: number;
  isAuth: boolean;
  login: string;
  name: string;
  lastName: string;
  email: string;
  joinDate: Date;
  birthDate: Date;
  token: string;
}

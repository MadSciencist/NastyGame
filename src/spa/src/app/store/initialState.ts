import IStore from "./IStore";

export const initialState: IStore = {
  user: {
    birthDate: new Date(),
    email: "",
    id: 0,
    joinDate: new Date(),
    lastName: "",
    login: "",
    name: "",
    nickname: ""
  }
};

export default initialState;

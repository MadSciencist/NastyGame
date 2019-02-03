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
    token: ""
  },
  player: {
    nickname: ""
  }
};

export default initialState;

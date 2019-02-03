import IStore from "./IStore";

export const initialState: IStore = {
  user: {
    birthDate: null,
    isAuth: false,
    email: "",
    id: 0,
    joinDate: null,
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

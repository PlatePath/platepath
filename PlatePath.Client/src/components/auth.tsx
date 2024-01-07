import { ReactNode, createContext, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";

interface IType {
  isLogged: () => boolean;
  getToken: () => string;
  setToken: (token: string) => void;
}
export const apiUrl =
  window.location.hostname === "localhost" || window.location.hostname === ""
    ? "http://localhost:3000/api"
    : "https://platepath.azurewebsites.net/api";
const authContext = createContext<IType>({
  isLogged: () => false,
  getToken: () => "",
  setToken: () => {},
});
export const useAuth = () => useContext(authContext);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [, rerender] = useState(false);
  const setToken = (jwt: string) => {
    sessionStorage.setItem("token", jwt);
    rerender((p) => !p);
  };
  const getToken = () => sessionStorage.getItem("token") as string;
  const isLogged = () => Boolean(sessionStorage.getItem("token"));
  return (
    <authContext.Provider value={{ isLogged, setToken, getToken }}>
      {children}
    </authContext.Provider>
  );
};
export const useRequireLogin = () => {
  const { isLogged } = useAuth();
  const navigate = useNavigate();
  if (!isLogged) {
    navigate("login");
  }
};

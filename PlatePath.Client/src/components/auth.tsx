import { ReactNode, createContext, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";

interface IType {
  isLogged: boolean;
  setLogged: React.Dispatch<React.SetStateAction<boolean>>;
}

const authContext = createContext<IType>({
  isLogged: false,
  setLogged: () => {},
});
export const useAuth = () => useContext(authContext);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [isLogged, setLogged] = useState(false);

  return (
    <authContext.Provider value={{ isLogged, setLogged }}>
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

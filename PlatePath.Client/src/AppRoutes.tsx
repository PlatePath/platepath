import { Routes, Route } from "react-router-dom";
import { BoxContainer } from "./components";
import Register from "./pages/Register/Register";
import Login from "./pages/Login/Login";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<BoxContainer>Test pr</BoxContainer>} />
      <Route path="/register" element={<Register />} />
      <Route path="/login" element={<Login />} />
    </Routes>
  );
};
export default AppRoutes;

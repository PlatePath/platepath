import { Routes, Route } from "react-router-dom";
import { BoxContainer } from "./components";
import Register from "./pages/Register/Register";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<BoxContainer>Test pr</BoxContainer>} />
      <Route path="/register" element={<Register />} />
    </Routes>
  );
};
export default AppRoutes;

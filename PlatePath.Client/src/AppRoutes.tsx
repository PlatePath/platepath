import { Routes, Route } from "react-router-dom";
import { BoxContainer } from "./components";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<BoxContainer>Test pr</BoxContainer>} />
    </Routes>
  );
};
export default AppRoutes;

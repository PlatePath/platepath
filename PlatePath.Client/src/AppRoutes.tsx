import { Routes, Route } from "react-router-dom";
import { BoxContainer } from "./components";
import Register from "./pages/Register/Register";
import Login from "./pages/Login/Login";
import MainLayout from "./components/MainLayout/MainLayout";
import Plans from "./pages/Plans/Plans";

const AppRoutes = () => {
  return (
    <Routes>
      <Route
        path="/"
        element={
          <MainLayout>
            <BoxContainer>Dashboard</BoxContainer>
          </MainLayout>
        }
      />
      <Route path="/forum" element={<MainLayout>Forum</MainLayout>} />
      <Route
        path="/plans"
        element={
          <MainLayout>
            <Plans />
          </MainLayout>
        }
      />
      <Route path="/register" element={<Register />} />
      <Route path="/login" element={<Login />} />
    </Routes>
  );
};
export default AppRoutes;

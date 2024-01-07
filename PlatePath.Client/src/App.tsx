import AppRoutes from "./AppRoutes";
import { AuthProvider } from "./components/auth";

function App() {
  return (
    <AuthProvider>
      <AppRoutes />
    </AuthProvider>
  );
}

export default App;

import React from "react";
import {
  BrowserRouter as Router,
  Route,
  Routes,
  Navigate,
} from "react-router-dom";
import Home from "./views/Home";
import GoogleLoginButton from "./components/GoogleLoginButton";

const App: React.FC = () => {
  const isAuthenticated = !!localStorage.getItem("jwt");

  return (
    <Router>
      <Routes>
        <Route path="/login" element={<GoogleLoginButton />} />

        <Route
          path="/home"
          element={isAuthenticated ? <Home /> : <Navigate to="/login" />}
        />

        <Route path="*" element={<Navigate to="/home" />} />
      </Routes>
    </Router>
  );
};

export default App;

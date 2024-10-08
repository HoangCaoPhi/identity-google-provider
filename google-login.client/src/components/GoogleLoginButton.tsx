import React from "react";
import { GoogleLogin } from "@react-oauth/google";
import { useNavigate } from "react-router-dom";
import { Box, Typography, Paper, Stack } from "@mui/material";

const GoogleLoginButton: React.FC = () => {
  const navigate = useNavigate();

  const handleLoginSuccess = (credentialResponse: any) => {
    console.log("Login Success:", credentialResponse);

    fetch("api/auth/google", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        token: credentialResponse.credential,
      }),
    })
      .then((res) => res.json())
      .then((data) => {
        console.log("JWT Token:", data.token);
        localStorage.setItem("jwt", data.token);
        navigate("/home");
      })
      .catch((error) => {
        console.error("Login failed:", error);
      });
  };

  const handleLoginError = () => {
    console.error("Login Failed");
  };

  return (
    <Box
      sx={{
        height: "100vh",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        backgroundColor: "#f5f5f5",
      }}
    >
      <Paper
        elevation={3}
        sx={{
          padding: 4,
          width: "350px",
          textAlign: "center",
          borderRadius: "10px",
        }}
      >
        <Typography variant="h5" gutterBottom>
          Welcome
        </Typography>
        <Typography variant="body1" color="textSecondary" paragraph>
          Please log in with Google to continue
        </Typography>
        <Stack spacing={2} direction="column">
          <GoogleLogin
            onSuccess={handleLoginSuccess}
            onError={handleLoginError}
            width="100%"
          />
        </Stack>
      </Paper>
    </Box>
  );
};

export default GoogleLoginButton;

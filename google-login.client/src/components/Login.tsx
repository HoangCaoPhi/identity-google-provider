import React from "react";
import { useGoogleLogin, useGoogleOneTapLogin } from "@react-oauth/google";
import { useNavigate } from "react-router-dom";
import { Box, Typography, Paper, Stack } from "@mui/material";

const Login: React.FC = () => {
  const navigate = useNavigate();


  const handleLoginSuccess = (token: any) => {
    console.log("Login Success:", token);

    fetch("api/auth/google", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        token: token,
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
 
  const login = useGoogleLogin({
    onSuccess: tokenResponse => handleLoginSuccess(tokenResponse?.access_token),
  });
  
  
  useGoogleOneTapLogin({
    onSuccess: credentialResponse => {
      handleLoginSuccess(credentialResponse?.credential)
    },
    onError: () => {
      console.log('Login Failed');
    },
  });  

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

        {/* Google Custom Login */}
        <Stack spacing={2} direction="column">
          <button onClick={() => login()}>Sign in with Google 🚀</button>
        </Stack>        
 
      </Paper>
    </Box>
  );
};

export default Login;

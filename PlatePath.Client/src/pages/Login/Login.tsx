import { Link, useNavigate } from "react-router-dom";
import { BoxContainer, Columns } from "../../components";
import { styled, Typography, Button, TextField } from "@mui/material";
import { apiUrl, useAuth } from "../../components/auth";
import { useEffect, useState } from "react";
const Half = styled("div")`
  width: 50%;
  height: 100vh;
`;

//TODO!: Separate into different components and apply theme
const Login = () => {
  const { isLogged, setToken } = useAuth();
  const [form, setForm] = useState({
    username: "",
    password: "",
  });
  const navigate = useNavigate();
  useEffect(() => {
    if (isLogged()) {
      navigate("/profile");
    }
  });
  const onSubmit = () => {
    fetch(`${apiUrl}/Auth/login`, {
      method: "POST",
      body: JSON.stringify(form),
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })
      .then((r) => r.json())
      .then((res) => {
        if (res.token) {
          setToken(res.token);
          navigate("/plans");
        } else {
          alert("Error");
        }
      })
      .catch((err) => alert(err));
  };
  const handleInputChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    id: string
  ) => {
    const target = event.target as HTMLInputElement;
    setForm({ ...form, [id]: target.value });
  };
  return (
    <BoxContainer>
      <Half sx={{ background: "#8DC63F" }}>
        <BoxContainer sx={{ alignItems: "center", height: "100%" }}>
          <Typography
            sx={{
              fontSize: "60px",
              color: "white",
              fontFamily: "cursive",
            }}
          >
            Plate Path
          </Typography>
        </BoxContainer>
      </Half>
      <Half>
        <BoxContainer
          sx={{
            height: "100%",
            justifyContent: "center",
            alignItems: "center",
            width: "100%",
          }}
        >
          <Columns
            sx={{
              width: "100%",
              maxWidth: "717px",
            }}
          >
            <Typography
              sx={{
                color: "#80C522",
                fontSize: "45px",
                fontWeight: "lighter",
                mb: "11px",
                lineHeight: "100%",
              }}
            >
              Welcome Back!
            </Typography>
            <Typography
              sx={{
                color: "#6D6D6D",
                fontSize: "30px",
                mb: "11px",
              }}
            >
              Login to get started
            </Typography>
            <Columns gap="30px" mt="50px" mb="30px">
              <TextField
                id="username"
                label="Username"
                value={form.username}
                onChange={(e) => handleInputChange(e, "username")}
              />
              <Columns>
                <TextField
                  id="password"
                  label="Password"
                  type="password"
                  value={form.password}
                  onChange={(e) => handleInputChange(e, "password")}
                />
                <Typography variant="subtitle2" color="grey" fontStyle="italic">
                  The password must contain 6 characters, 1 Uppercase, 1
                  Lowercase and one special symbol
                </Typography>
              </Columns>
            </Columns>
            <Button
              onClick={onSubmit}
              variant="contained"
              sx={{
                background: "#8DC63F",
                width: "100%",
                mt: "20px",
                fontSize: "24px",
                textTransform: "none",
              }}
            >
              Login
            </Button>
            <Typography
              component={Link}
              to="/register"
              sx={{
                ml: "auto",
                mt: "3px",
                color: "#8DC63F",
                fontStyle: "italic",
              }}
            >
              or register
            </Typography>
            <BoxContainer
              sx={{
                width: "100%",
                alignItems: "center",
                gap: "53px",
              }}
            >
              <Typography
                sx={{
                  color: "#8DC63F",
                  textDecoration: "underline",
                  cursor: "pointer",
                }}
              >
                Terms & Conditions
              </Typography>
              <Typography
                sx={{
                  color: "#8DC63F",
                  textDecoration: "underline",
                  cursor: "pointer",
                }}
              >
                Privacy Policy
              </Typography>
            </BoxContainer>
          </Columns>
        </BoxContainer>
      </Half>
    </BoxContainer>
  );
};
export default Login;

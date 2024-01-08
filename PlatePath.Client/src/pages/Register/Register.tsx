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
const Register = () => {
  const { isLogged } = useAuth();
  const [form, setForm] = useState({
    username: "",
    email: "",
    password: "",
  });
  const handleInputChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    id: string
  ) => {
    const target = event.target as HTMLInputElement;
    setForm({ ...form, [id]: target.value });
  };
  const navigate = useNavigate();
  useEffect(() => {
    if (isLogged()) {
      navigate("/profile");
    }
  });
  const onSubmit = () => {
    fetch(`${apiUrl}/Auth/register`, {
      method: "POST",
      body: JSON.stringify({
        ...form,
        age: 18,
        heightCm: 180,
        weightKg: 80,
        activityLevel: 1,
        gender: 1,
        weightGoal: 1,
      }),
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })
      .then((r) => r.json())
      .then((res) => {
        if (res.success) {
          navigate("/login");
        }
      })
      .catch((err) => err);
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
              Welcome!
            </Typography>
            <Typography
              sx={{
                color: "#6D6D6D",
                fontSize: "30px",
                mb: "11px",
              }}
            >
              Register to get started
            </Typography>
            <Columns gap="30px" mt="50px" mb="30px">
              <TextField
                id="email"
                label="Email Address"
                value={form.email}
                onChange={(e) => handleInputChange(e, "email")}
              />
              <TextField
                id="username"
                label="Username"
                value={form.username}
                onChange={(e) => handleInputChange(e, "username")}
              />
              <TextField
                id="password1"
                label="Password"
                type="password"
                value={form.password}
                onChange={(e) => handleInputChange(e, "password")}
              />
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
              Register
            </Button>
            <Typography
              component={Link}
              to="/login"
              sx={{
                ml: "auto",
                mt: "3px",
                color: "#8DC63F",
                fontStyle: "italic",
              }}
            >
              or login with your account
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
export default Register;

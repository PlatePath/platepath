import { Link, useNavigate } from "react-router-dom";
import { BoxContainer, Columns } from "../../components";
import { styled, Typography, Button, TextField } from "@mui/material";
import { useAuth } from "../../components/auth";
import { useEffect } from "react";
const Half = styled("div")`
  width: 50%;
  height: 100vh;
`;

//TODO!: Separate into different components and apply theme
const Register = () => {
  const { isLogged, setLogged } = useAuth();
  const navigate = useNavigate();
  useEffect(() => {
    if (isLogged) {
      navigate("/profile");
    }
  });
  const onSubmit = () => {
    setLogged(true);
    navigate("/plans");
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
              <TextField id="email" label="Email Address" />
              <TextField id="name" label="Name" />
              <TextField id="password1" label="Password" />
              <TextField id="password2" label="Repeat Password" />
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

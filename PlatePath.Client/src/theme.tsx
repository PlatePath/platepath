import { createTheme } from "@mui/material";
declare module "@mui/material/styles" {
  interface Theme {
    colors: {
      green: string;
      white: string;
      text1: string;
      text2: string;
      text3: string;
    };
  }
  // allow configuration using `createTheme`
  interface ThemeOptions {
    colors?: {
      green?: string;
      white?: string;
      text1?: string;
      text2?: string;
      text3?: string;
    };
  }
  interface TypographyVariants {
    title20: React.CSSProperties;
    subtitle: React.CSSProperties;
  }

  // allow configuration using `createTheme`
  interface TypographyVariantsOptions {
    title20?: React.CSSProperties;
    subtitle?: React.CSSProperties;
  }
}
declare module "@mui/material/Typography" {
  interface TypographyPropsVariantOverrides {
    title20: true;
    subtitle: true;
  }
}
const theme = createTheme({
  colors: {
    green: "#8DC63F",
    white: "#110e0e",
    text1: "#191B19",
    text2: "#494949",
    text3: "#382867E",
  },
  typography: {
    title20: {
      color: "#191B19",
      fontFamily: "Roboto",
      fontSize: "20px",
      fontStyle: "normal",
      fontWeight: "600",
      lineHeight: "normal",
    },
    subtitle: {
      color: "#82867E",
      fontFamily: "Roboto",
      fontSize: "14px",
      fontStyle: "normal",
      fontWeight: "600  ",
      lineHeight: "normal",
    },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: ({ ownerState }) => ({
          ...(ownerState.variant === "text" && {
            color: "#494949",
            fontWeight: 600,
            "&:hover": {
              backgroundColor: "#8DC63F",
              color: "#fff !important",
            },
            "& a": {
              color: "inherit",
            },
          }),
        }),
      },
    },
  },
});
export default theme;

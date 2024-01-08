import { useState } from "react";
import { BoxContainer, Columns } from "../../components";
import { apiUrl, useAuth } from "../../components/auth";
import {
  Alert,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";
import { Plan } from "./Plans";

const PlansForm = () => {
  const { getToken } = useAuth();
  const [formData, setFormData] = useState<Plan>({
    mealPlanName: "",
    days: 5,
    mealsPerDay: 3,
    minCalories: 1800,
    maxCalories: 3000,
    proteins: 0,
    fats: 0,
    carbohydrates: 0,
    dietType: "dairy-free",
  });
  let currentTimeout;
  const [showAlert, setShowAlert] = useState(false);
  const onSubmit = () => {
    console.log(getToken());
    const token = getToken();
    const myHeaders = new Headers({
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    });
    fetch(`${apiUrl}/MealPlans/generate`, {
      method: "POST",
      body: JSON.stringify(formData),
      headers: myHeaders,
    })
      .then((r) => r.json())
      .then((res) => {
        console.log(res);
        currentTimeout = null;
        setShowAlert(true);
        currentTimeout = setTimeout(() => {
          setShowAlert(false);
        }, 5000);
      })
      .catch((err) => err);
  };
  const handleInputChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    id: string
  ) => {
    const target = event.target as HTMLInputElement;
    if (
      id === "mealPlanName" ||
      id === "dietType" ||
      target.value.match(/^[0-9]+$/) ||
      target.value === ""
    )
      setFormData({ ...formData, [id]: target.value });
  };
  return (
    <Columns gap="20px">
      {showAlert ? (
        <Alert severity="success">Plan submitted successfully</Alert>
      ) : null}
      <TextField
        label="Name"
        value={formData.mealPlanName}
        onChange={(e) => handleInputChange(e, "mealPlanName")}
      />
      <TextField
        label="Days"
        value={formData.days}
        onChange={(e) => handleInputChange(e, "days")}
      />
      <TextField
        label="Meals per day"
        value={formData.mealsPerDay}
        onChange={(e) => handleInputChange(e, "mealsPerDay")}
      />
      <BoxContainer gap="10px">
        <TextField
          label="Min Calories"
          value={formData.minCalories}
          onChange={(e) => handleInputChange(e, "minCalories")}
        />
        <TextField
          label="Max Calories"
          value={formData.maxCalories}
          onChange={(e) => handleInputChange(e, "maxCalories")}
        />
      </BoxContainer>
      <BoxContainer gap="10px">
        <TextField
          label="Proteins"
          value={formData.proteins}
          onChange={(e) => handleInputChange(e, "proteins")}
        />
        <TextField
          label="Carbohydrates"
          value={formData.carbohydrates}
          onChange={(e) => handleInputChange(e, "carbohydrates")}
        />
        <TextField
          label="Fats"
          value={formData.fats}
          onChange={(e) => handleInputChange(e, "fats")}
        />
      </BoxContainer>
      <FormControl fullWidth sx={{ mt: "10px" }}>
        <InputLabel>Diet Type</InputLabel>
        <Select
          name="dietType"
          value={formData.dietType}
          onChange={(e) =>
            handleInputChange(
              e as React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
              "dietType"
            )
          }
        >
          <MenuItem value="dairy-free">Dairy Free</MenuItem>
          <MenuItem value="gluten-free">Gluten Free</MenuItem>
          <MenuItem value="pork-free">Pork Free</MenuItem>
          <MenuItem value="soy-free">Soy Free</MenuItem>
          <MenuItem value="wheat-free">Wheat Free</MenuItem>
          <MenuItem value="peanut-free">Peanut Free</MenuItem>
          <MenuItem value="keto-friendly">Keto Friendly</MenuItem>
          <MenuItem value="vegetarian">Vegetarian</MenuItem>
          <MenuItem value="vegan">Vegan</MenuItem>
          <MenuItem value="paleo">Paleo</MenuItem>
        </Select>
      </FormControl>
      <Button variant="contained" onClick={onSubmit}>
        Submit Plan
      </Button>
    </Columns>
  );
};
export default PlansForm;

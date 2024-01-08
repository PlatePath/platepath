import { useEffect, useState } from "react";
import { BoxContainer, Columns } from "../../components";
import { Plan } from "./Plans";
import { Box, Typography } from "@mui/material";

const PlanCard = ({
  mealPlanName,
  days,
  mealsPerDay,
  minCalories,
  maxCalories,
  proteins,
  fats,
  carbohydrates,
  dietType,
}: Plan) => {
  return (
    <Columns
      sx={{
        borderLeft: "2px solid #8DC63F",
      }}
    >
      <Typography variant="h6" paddingLeft={1}>
        {mealPlanName}
      </Typography>
      <Columns
        sx={{
          gap: "6px",
          background: "#8DC63F",
          color: "white",
          padding: "10px",
          fontWeight: 600,
          borderTopRightRadius: "4px",
          borderBottomRightRadius: "4px",
        }}
      >
        <Typography fontWeight={600}>Days: {days}</Typography>
        <Typography fontWeight={600}>Meals Per Day: {mealsPerDay}</Typography>
        <Typography fontWeight={600}>Min Calories: {minCalories}</Typography>
        <Typography fontWeight={600}>Max Calories: {maxCalories}</Typography>
        <Typography fontWeight={600}>Proteins: {proteins}</Typography>
        <Typography fontWeight={600}>Fats: {fats}</Typography>
        <Typography fontWeight={600}>Carbs: {carbohydrates}</Typography>
        <Typography fontWeight={600}>Diet Type: {dietType}</Typography>
      </Columns>
    </Columns>
  );
};

const PlansList = () => {
  const [plans, setPlans] = useState<Plan[]>([
    {
      mealPlanName: "Some Name",
      days: 5,
      mealsPerDay: 3,
      minCalories: 1800,
      maxCalories: 3000,
      proteins: 0,
      fats: 0,
      carbohydrates: 0,
      dietType: "dairy-free",
    },
    {
      mealPlanName: "Some Name",
      days: 5,
      mealsPerDay: 3,
      minCalories: 1800,
      maxCalories: 3000,
      proteins: 0,
      fats: 0,
      carbohydrates: 0,
      dietType: "dairy-free",
    },
    {
      mealPlanName: "Some Name",
      days: 5,
      mealsPerDay: 3,
      minCalories: 1800,
      maxCalories: 3000,
      proteins: 0,
      fats: 0,
      carbohydrates: 0,
      dietType: "dairy-free",
    },
    {
      mealPlanName: "Some Name",
      days: 5,
      mealsPerDay: 3,
      minCalories: 1800,
      maxCalories: 3000,
      proteins: 0,
      fats: 0,
      carbohydrates: 0,
      dietType: "dairy-free",
    },
    {
      mealPlanName: "Some Name",
      days: 5,
      mealsPerDay: 3,
      minCalories: 1800,
      maxCalories: 3000,
      proteins: 0,
      fats: 0,
      carbohydrates: 0,
      dietType: "dairy-free",
    },
    {
      mealPlanName: "Some Name",
      days: 5,
      mealsPerDay: 3,
      minCalories: 1800,
      maxCalories: 3000,
      proteins: 0,
      fats: 0,
      carbohydrates: 0,
      dietType: "dairy-free",
    },
  ]);
  useEffect(() => {}, []);
  return (
    <Box
      sx={{
        width: "100%",
        maxHeight: "80%",
        overflow: "scroll",
      }}
    >
      <BoxContainer
        sx={{
          flexWrap: "wrap",
          justifyContent: "space-between",
          width: "100%",
          gap: "30px",
          "&>div": {
            flex: "1 1 160px",
          },
        }}
      >
        {plans.map((plan, i) => (
          <PlanCard {...plan} key={i} />
        ))}
      </BoxContainer>
    </Box>
  );
};
export default PlansList;

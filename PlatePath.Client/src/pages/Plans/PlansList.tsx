import { useEffect, useState } from "react";
import { BoxContainer, Columns } from "../../components";
import { Plan } from "./Plans";
import {
  Box,
  Typography,
  Autocomplete,
  TextField,
  Divider,
} from "@mui/material";

type Meal = {
  id: number;
  post: null;
  edamamId: string;
  name: string;
  kcal: number;
  servings: number;
  carbohydrates: number;
  fats: number;
  protein: number;
  ingredientLines: string;
  imageURL: string;
};
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
  const [names, setNames] = useState(["Kopele"]);
  const [meal, setMeal] = useState<Meal>({
    id: 108,
    post: null,
    edamamId: "630d1df1686cca0c08fda0f4dbb53855",
    name: "THE BEST INSTANT POT MEATLOAF",
    kcal: 763,
    servings: 4,
    carbohydrates: 57,
    fats: 44,
    protein: 38,
    ingredientLines:
      "1.5 lbs Boomer Gold Little Potatoes\r\n1 cup chicken broth or water\r\n2 tsp minced garlic\r\n4 tbsp salted butter sliced\r\n1/2 cup shredded Parmesan cheese\r\ndried parsley optional\r\nFor the meatloaf:\r\n1 pound lean ground beef 93% lean\r\n3/4 cup milk\r\n1 egg\r\n3 slices bread cut up into small pieces\r\n1 tablespoon Worcestershire sauce\r\n½ tablespoon onion powder\r\n½ teaspoon salt\r\n½ teaspoon dry ground mustard\r\n¼ teaspoon black pepper\r\n⅛ teaspoon garlic powder\r\nFor the meatloaf glaze:\r\n½ cup ketchup\r\n1 tbsp. balsamic vinegar optional",
    imageURL:
      "https://platepathstorage01.blob.core.windows.net/platepathblobs/630d1df1686cca0c08fda0f4dbb53855",
  });
  const getNames = () => {};
  useEffect(() => {}, []);
  return (
    <>
      <Autocomplete
        disablePortal
        id="combo-box-demo"
        options={names}
        sx={{ width: 300 }}
        renderInput={(params) => (
          <TextField {...params} label="Meal Plan Names" />
        )}
      />
      <Divider sx={{ my: "10px" }} />
      {meal ? (
        <>
          <BoxContainer
            sx={{
              justifyContent: "flex-start",
              width: "100%",
            }}
          >
            <img
              src={meal.imageURL}
              alt="meal img"
              width="250px"
              height="250px"
            />
            <Columns gap="7px" ml="15px">
              <Typography variant="h6">Calories: {meal.kcal}kcal</Typography>
              <Typography variant="h6">Fats: {meal.fats}g</Typography>
              <Typography variant="h6">
                Carbohydrates: {meal.carbohydrates}g
              </Typography>
              <Typography variant="h6">Protein: {meal.protein}g</Typography>
            </Columns>
          </BoxContainer>
          <Columns
            sx={{
              alignItems: "flex-start",
              width: "100%",
              whiteSpace: "pre-line",
              maxHeight: "500px",
              overflow: "scroll",
            }}
          >
            <Columns mb="20px">
              <Typography variant="h5">Needed Ingredients</Typography>
              <Divider />
            </Columns>
            <Typography variant="h6" fontStyle="italic">
              {meal.ingredientLines}
            </Typography>
          </Columns>
        </>
      ) : null}
      {/* <BoxContainer
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
      </BoxContainer> */}
    </>
  );
};
export default PlansList;

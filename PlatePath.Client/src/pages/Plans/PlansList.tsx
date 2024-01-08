import { useEffect, useState } from "react";
import { BoxContainer, Columns } from "../../components";
import { Plan } from "./Plans";
import { Typography, Autocomplete, TextField, Divider } from "@mui/material";
import { apiUrl, useAuth } from "../../components/auth";

type Recipe = {
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

const PlansList = () => {
  const { getToken } = useAuth();
  const [names, setNames] = useState(["Kopele"]);
  const [recipes, setRecipes] = useState<Recipe[]>([]);
  const [recipe, setRecipe] = useState<Recipe>({
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
  useEffect(() => {
    const token = getToken();
    const myHeaders = new Headers({
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    });
    fetch(`${apiUrl}/MealPlans/getAll`, {
      method: "GET",
      headers: myHeaders,
    })
      .then((r) => r.json())
      .then((res) => {
        if (res.mealPlanNames) {
          setNames(res.mealPlanNames);
        }
      })
      .catch((err) => err);
  }, []);
  const onSelect = (e: React.SyntheticEvent<Element, Event>) => {
    const target = e.target as HTMLLIElement;

    const token = getToken();
    const myHeaders = new Headers({
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    });
    fetch(`${apiUrl}/MealPlans/${target.textContent}`, {
      method: "GET",
      headers: myHeaders,
    })
      .then((r) => r.json())
      .then((res) => {
        console.log(res);
        if (res.mealPlan?.meals) {
          setRecipes(res.mealPlan?.meals);
        }
      })
      .catch((err) => err);
  };
  const onSelectRecipe = (e: React.SyntheticEvent<Element, Event>) => {
    const target = e.target as HTMLLIElement;
    console.log(target);
    const recipe = recipes.find((obj) => obj.name === target.textContent);
    if (recipe) {
      setRecipe({
        ...recipe,
        ingredientLines: recipe.ingredientLines.replaceAll("&&", "\n"),
      });
    }
  };
  return (
    <>
      <BoxContainer
        gap="30px"
        sx={{
          justifyContent: "flex-start",
        }}
      >
        <Autocomplete
          disablePortal
          id="combo-box-demo"
          options={names}
          onChange={(e) => onSelect(e)}
          sx={{ width: 300 }}
          renderInput={(params) => <TextField {...params} label="Meal Plans" />}
        />
        {recipes.length ? (
          <Autocomplete
            disablePortal
            id="combo-box-demo"
            options={recipes}
            onChange={(e) => onSelectRecipe(e)}
            sx={{ width: 300 }}
            renderInput={(params) => (
              <TextField {...params} label="Meal Plans" />
            )}
            getOptionLabel={(option: Recipe) => option.name}
          />
        ) : null}
      </BoxContainer>
      <Divider sx={{ my: "10px" }} />
      {recipe ? (
        <>
          <BoxContainer
            sx={{
              justifyContent: "flex-start",
              width: "100%",
            }}
          >
            <img
              src={recipe.imageURL}
              alt="meal img"
              width="250px"
              height="250px"
            />
            <Columns gap="7px" ml="15px">
              <Typography variant="h6">Calories: {recipe.kcal}kcal</Typography>
              <Typography variant="h6">Fats: {recipe.fats}g</Typography>
              <Typography variant="h6">
                Carbohydrates: {recipe.carbohydrates}g
              </Typography>
              <Typography variant="h6">Protein: {recipe.protein}g</Typography>
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
              {recipe.ingredientLines}
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

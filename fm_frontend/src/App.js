import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./components/HomeComponents/Home.jsx";
import Matches from "./components/MatchComponents/Matches";
import Players from "./components/PlayerComponents/Players";
import Match from "./components/MatchComponents/Match";
import Player from "./components/PlayerComponents/Player";
import Team from "./components/TeamComponents/Team";
import Profile from "./components/ProfileComponents/Profile";
import Login from "./components/AuthComponents/SignInPage";
import Header from "./components/HeaderComponents/Header.jsx";
import SignUp from "./components/AuthComponents/SignUpPage.jsx";
import Leagues from "./components/LeagueComponents/Leagues.jsx";
import Users from "./components/UsersComponents/Users.jsx";

const App = () => {
  
  return <>
      <Router>
        <Header />
        <Routes>
            <Route path="/users" element={<Users />}/>
            <Route path="/home" element={<Home />}/>
            <Route path="/players" element={<Players />}/>
            <Route path="/players/:id" element={<Player />}/>
            <Route path="/leagues/:id" element={<Leagues />}/>
            <Route path="/matches" element={<Matches />}/>
            <Route path="/matches/:id" element={<Match />}/>
            <Route path="/teams/:id" element={<Team />}/>
            <Route path="/profile" element={<Profile />}/>
            <Route path="/" element={<Login />}/>
            <Route path="/signup" element={<SignUp />}/>
        </Routes>
      </Router>
  </>;
}

export default App;

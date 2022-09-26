import React from "react";
import Header from "./components/Header";
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import Home from "./components/HomeComponents/Home.jsx";
import Matches from "./components/MatchComponents/Matches";
import Leagues from "./components/LeagueComponents/Leagues";
import Players from "./components/PlayerComponents/Players";
import Match from "./components/MatchComponents/Match";
import Player from "./components/PlayerComponents/Player";
import Team from "./components/TeamComponents/Team";
import Profile from "./components/ProfileComponents/Profile";

function App() {
  return <>
    <Router>
    <Header></Header>
        <Routes>
            <Route path="/" element={<Home />}/>
            <Route path="/leagues" element={<Leagues />}/>
            <Route path="/players" element={<Players />}/>
            <Route path="/players/:id" element={<Player />}/>
            <Route path="/matches" element={<Matches />}/>
            <Route path="/matches/:id" element={<Match />}/>
            <Route path="/teams/:id" element={<Team />}/>
            <Route path="/profile" element={<Profile />}/>
        </Routes>
      </Router>
  </>;
}

export default App;

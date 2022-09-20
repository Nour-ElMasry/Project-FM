import React from "react";
import Header from "./components/Header";
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import Home from "./components/HomeComponents/Home.jsx";
import Matches from "./components/MatchComponents/Matches";
import Leagues from "./components/LeagueComponents/Leagues";
import Teams from "./components/TeamComponents/Teams";

function App() {
  return <>
    <Router>
    <Header></Header>
        <Routes>
            <Route path="/" element={<Home />}/>
            <Route path="/leagues" element={<Leagues />}/>
            <Route path="/teams" element={<Teams />}/>
            <Route path="/matches" element={<Matches />}/>
        </Routes>
      </Router>
  </>;
}

export default App;

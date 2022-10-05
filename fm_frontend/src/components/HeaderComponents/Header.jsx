
import { useLocation } from "react-router-dom";
import AuthHeader from "./AuthHeader";
import NormalHeader from "./NormalHeader";

const Header = () => {
  const location = useLocation();

  if (location.pathname === "/" || location.pathname.includes("/signup"))
    return <AuthHeader />

  return <NormalHeader /> 

};

export default Header;
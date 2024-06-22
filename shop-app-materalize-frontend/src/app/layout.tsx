import type {Metadata} from "next";
import {Inter} from "next/font/google";
import "./globals.css";
import {Navbar} from "@/app/shared/Navbar";
import Footer from "@/app/shared/Footer";

const inter = Inter({subsets: ["latin"]});

export const metadata: Metadata = {
    title: "Create Next App",
    description: "Generated by create next app",
};

export default function RootLayout({
                                       children,
                                   }: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en">
        <body className={inter.className}>
        <div id="page-container">
        <Navbar/>
        <main>{children}</main>
        </div>
        <Footer/>

        </body>
        </html>
    );
}
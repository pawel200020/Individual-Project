import type {Metadata} from "next";
import {Inter} from "next/font/google";
import "./globals.css";
import {Navbar} from "@/app/shared/Navbar";
import Footer from "@/app/shared/Footer";
import {ThemeProvider} from "@/app/context/ThemeContext";
import ClientThemeWrapper from "@/app/context/ClientThemeWrapper";

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
        <ThemeProvider>
            <ClientThemeWrapper>
                <div className="ThemedBody">
                    <div className="navbar">
                        <Navbar/>
                    </div>
                    <div className="content">
                        <main>{children}</main>
                    </div>
                    <div className="footer">
                        <Footer/>
                    </div>
                </div>
            </ClientThemeWrapper>
        </ThemeProvider>
        </body>
        </html>

    );
}
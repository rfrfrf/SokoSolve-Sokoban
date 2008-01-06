<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
				xmlns="http://www.w3.org/1999/xhtml"
				xmlns:f="http://sokosolve.sf.net/FeatureSet.xsd"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
	<html xmlns="http://www.w3.org/1999/xhtml">
		<head>
			<link type="text/css" rel="Stylesheet" href="style.css" />
		</head>
    <body>
		<xsl:apply-templates/>
	</body>
</html>
</xsl:template>

	<xsl:template match="f:FeatureSet">
		<h1>
			Feature set
		</h1>
		<ul>
			<xsl:apply-templates/>
		</ul>
	</xsl:template>

	<xsl:template match="f:Feature">
		<li>
			<b>
				<a>
					<xsl:attribute name='name'><xsl:value-of select='@FeatureID'/></xsl:attribute>
				 <xsl:value-of select="@Name"/>
				</a>
			</b>
			<span style="color:red;">
				[<xsl:value-of select="@Status"/>]
			</span>
			<xsl:value-of select="f:Description"/>
			<xsl:apply-templates select="f:Author"/>
			<xsl:apply-templates select="f:Links"/>


			<xsl:if  test="f:Feature">
				<ul>
					<xsl:apply-templates select="f:Feature"/>
				</ul>
			</xsl:if>
		</li>
	</xsl:template>

	<xsl:template match="f:Author">
		<br/><b>Author</b>: <xsl:value-of select="@Name"/> &#160; <xsl:value-of select="@Email"/>
	</xsl:template>

	<xsl:template match="f:Link">
		<xsl:comment>Start Link</xsl:comment>
		<xsl:variable name="FeatureREF" select="@FeatureREF"/>
		<br/><b>Link to </b>:
		<a>
			<xsl:attribute name='href'>#<xsl:value-of select="$FeatureREF"/></xsl:attribute>
			<xsl:value-of select="//f:Feature[@FeatureID=$FeatureREF]/@Name"/>
		</a>
		 "<xsl:value-of select="@Description"/>" (<xsl:value-of select="@Type"/>)		
		 <xsl:comment>End Link</xsl:comment>
	</xsl:template>

</xsl:stylesheet> 
